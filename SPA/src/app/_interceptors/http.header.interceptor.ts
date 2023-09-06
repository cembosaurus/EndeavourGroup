import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';





@Injectable()
export class HttpHeaderInterceptor implements HttpInterceptor {

  constructor() {}

  
  intercept(req: HttpRequest<any>, next: HttpHandler) {

    // cloned headers.
    req = req.clone({
      setHeaders: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json'
      }
    });
    
    return next.handle(req);
  }
}