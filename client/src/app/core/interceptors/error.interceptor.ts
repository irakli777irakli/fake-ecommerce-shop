import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NavigationExtras, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    
    constructor(
        private router: Router,
        private toastr: ToastrService) { }

    // req => request, next => response
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if(error) {
                    // validation errors
                    if(error.status === 400) {
                        if(error.error.errors) {
                            // validation error
                            // passing error object to same component, where it was triggered
                            throw error.error;
                        }else {
                            // bad-request
                            this.toastr.error(error.error.message,error.error.statusCode);
                        }
                    }
                    if(error.status === 401) {
                        this.toastr.error(error.error.message,error.error.statusCode);
                    }
                    // not-found errors
                    if(error.status === 404) {
                        this.router.navigateByUrl('/not-found');
                    }

                    // server errors
                    if(error.status === 500) {
                        // passing state from this component, to routed component
                        const navigationExtras: NavigationExtras = {state: {error: error.error}};
                        this.router.navigateByUrl('/server-error',navigationExtras);
                    }

                    return throwError(error);
                }
            })
        )
    }

}
