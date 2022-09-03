import { InjectionToken, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { Contracts } from '../api/ContractApi';
import { environment } from 'src/environments/environment';

const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');
@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule ],
  providers: [
    {provide: API_BASE_URL, useValue: environment.API_BASE_URL},
    Contracts.Client],
  bootstrap: [AppComponent],
})
export class AppModule {}
