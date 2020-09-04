import { AuthConfig } from 'angular-oauth2-oidc';

export const authCodeFlowConfig: AuthConfig = {
  // identity provider url
  issuer: 'https://localhost:40000',

  // url of the spa to redirect the user after login
  redirectUri: window.location.origin,

  // spa client id registered with identityserver
  clientId: 'spa',

  responseType: 'code',

  scope: 'openid ApiOne',

  showDebugInformation: true,
};
