import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { HttpClient, HttpClientModule, HttpFeature, HttpFeatureKind, provideHttpClient, withFetch } from '@angular/common/http';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),provideHttpClient(withFetch()),
  
  importProvidersFrom(HttpClientModule),importProvidersFrom(TranslateModule.forRoot({
  defaultLanguage:'en',
  loader:{
    provide:TranslateLoader,
    useFactory:httpLoaderFactory,
    deps:[HttpClient]
  }

  }))]
   
};



export function httpLoaderFactory(http: HttpClient){

  return new TranslateHttpLoader(http,'./assets/i18n/','.json')
}





