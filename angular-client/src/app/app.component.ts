import { authCodeFlowConfig } from './auth-config';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  authconfig: AuthConfig = authCodeFlowConfig;

  constructor(
    private oauthService: OAuthService,
    private httpClient: HttpClient
  ) {}

  ngOnInit(): void {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  login(): void {
    this.oauthService.initCodeFlow();
  }

  callApi(): void {
    this.httpClient.get('https://localhost:41000/secret').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
