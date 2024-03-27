import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
