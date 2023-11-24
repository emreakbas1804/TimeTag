import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable()
export class RequestHeaderInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const jwtToken = localStorage.getItem("accessToken");
    if (jwtToken) {
      const clonedRequest = request.clone({
        setHeaders: {
          "Authorization": `Bearer ${jwtToken}`,
          "AccessToken": "1234567!"
        }
      })
      return next.handle(clonedRequest);
    }
    // if (!request.headers.has('Content-Type')) {
    //   // "Content-Type" başlığını ekleyerek klonla
    //   request = request.clone({
    //     setHeaders: {
    //       'Content-Type': 'application/json'
    //     }
    //   });
    // }

    // İleriye devam et
    return next.handle(request);


  }
}
