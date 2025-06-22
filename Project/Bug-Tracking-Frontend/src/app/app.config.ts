import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { authService } from './core/Services/authService';
import { provideHttpClient } from '@angular/common/http';
import { authGuard } from './auth-guard';
import { bugService } from './core/Services/bugService';
import { empManageService } from './core/Services/empManageService';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    authService,
    authGuard,
    bugService,
    empManageService
  ]
};
