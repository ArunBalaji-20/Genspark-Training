import { HttpClient, HttpHeaders } from "@angular/common/http";
import { computed, inject, Injectable, signal } from "@angular/core";

@Injectable()
export class authService
{
    private readonly _role = signal<string>('');
    private readonly _email=signal<string>('');
    private http= inject(HttpClient);
    router: any;

  constructor() {
    // When service loads, check if cookie exists and parse JWT
   const token = this.getCookie('access_token');
    if (token) {
      const payload = this.parseJwt(token);
      if (payload) {
        this._role.set(payload?.role || '');
      }
    }
  }

  // Expose role as signal
  role = computed(() => this._role());
  email=computed(()=> this._email());

  // Public API
  isLoggedIn(): boolean {
    return this._role() !== '';
  }

  logout(): void {
    this._role.set('');
    this.deleteCookie('access_token');
    this.deleteCookie('refresh_token')
    // optionally navigate to login page
  }

 loginAPI(email: string, password: string) {
  return this.http.post<{ accessToken: string ,refreshToken:string}>(
    'http://localhost:5258/api/v1/Authentication/login',
    { email, password },
    { observe: 'response' }
  );
}

logoutAPI() {
  const token = this.getCookie('access_token');  
  const refreshToken = this.getCookie('refresh_token');

  return this.http.post<{ refreshToken: string }>(
    'http://localhost:5258/api/v1/Authentication/logout',
    { refreshToken: refreshToken }, 
    {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}`
      }),
      observe: 'response'
    }
  );
}


  // Called after successful login (when backend returns JWT)
  setAuthToken(token: string,refreshToken:string): void {
    this.setCookie('access_token', token, 1); // 1-day expiry
    this.setCookie('refresh_token',refreshToken,7)
    const payload = this.parseJwt(token);
    this._role.set(payload?.role || '');
    this._email.set(payload?.email || '');
  }

  // JWT decode (payload only)
  private parseJwt(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(atob(base64).split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));
      return JSON.parse(jsonPayload);
    } catch (error) {
      console.error('Invalid JWT:', error);
      return null;
    }
  }

  // Cookie helpers
  private setCookie(name: string, value: string, days: number): void {
    const expires = new Date(Date.now() + days * 86400000).toUTCString();
    document.cookie = `${name}=${value}; expires=${expires}; path=/`;
  }

 

  private getCookie(name: string): string | null {
    const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    return match ? match[2] : null;
  }

  private deleteCookie(name: string): void {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  }
}


