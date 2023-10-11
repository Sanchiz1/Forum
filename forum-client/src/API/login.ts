import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, map, mergeMap, Observable, of, timer } from "rxjs";
import { redirect } from "react-router-dom";

const url = "https://localhost:7295/graphql-login";


export type TokenType = {
  issued_at: Date,
  value: string
  expires_at: Date,
}

export type LoginType = {
  data: {
    login: {
      user_id: string,
      redirect_url: string,
      access_token: TokenType,
      refresh_token: TokenType,
    }
  }
}


export function ajaxForLogin(variables: {}) {
  return ajax<LoginType>({
    url: url,
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify({
      query: `query($login:CredentialsInput!){
          login(login:$login){
            access_token {
              issued_at
              value
              expires_at
            }
            user_id
            refresh_token {
              issued_at
              value
              expires_at
            }
          }
        }`,
      variables
    }),
    withCredentials: true,
  }).pipe(
    map((value): string => {


      let fullResponse = value.response;
      let response = fullResponse.data.login;




      setCookie({
        name: "access_token",
        value: JSON.stringify(response.access_token),
        expires_second: (new Date(response.access_token.expires_at).getTime() - new Date(response.access_token.issued_at).getTime()) / 1000,
        path: "/"
      });
      setCookie({
        name: "user_id",
        value: response.user_id,
        expires_second: (new Date(response.access_token.expires_at).getTime() - new Date(response.access_token.issued_at).getTime()) / 1000,
        path: "/"
      });
      setCookie({
        name: "refresh_token",
        value: JSON.stringify(response.refresh_token),
        expires_second: (new Date(response.refresh_token.expires_at).getTime() - new Date(response.refresh_token.issued_at).getTime()) / 1000,
        path: "/"
      });

  return "/";
}),
catchError((error) => {
  throw error
})
  );
}




export type setCookieParamas = {
  name: string,
  value: string,
  expires_second?: number,
  path?: string,
  domain?: string,
  secure?: boolean
}

export function getCookie(name: string) {
  name = name + "=";
  let ca = document.cookie.split(';');
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return decodeURIComponent(c.substring(name.length, c.length));
    }
  }
  return null;
}

export function setCookie(cookieParams: setCookieParamas) {
  let s = cookieParams.name + '=' + encodeURIComponent(cookieParams.value) + ';';
  if (cookieParams.expires_second) {
    let d = new Date();
    const offset = d.getTimezoneOffset() * 60;
    cookieParams.expires_second -= offset;
    d.setTime(d.getTime() + cookieParams.expires_second * 1000);
    s += ' expires=' + d.toUTCString() + ';';
  }
  if (cookieParams.path) s += ' path=' + cookieParams.path + ';';
  if (cookieParams.domain) s += ' domain=' + cookieParams.domain + ';';
  if (cookieParams.secure) s += ' secure;';
  document.cookie = s;
}

export function getTokenOrNavigate(isLoginRedirect: boolean = false) {
  console.log("check")
  const token = getCookie("refresh_token");
  if (!token && !isLoginRedirect) {
    return redirect("/Login");
  } else if (isLoginRedirect && token)
    return redirect("/");

  return token;
}

export const toDateTime = (secs: number) => {
  var t = new Date(1970, 0, 1);
  t.setSeconds(secs);
  return t;
}



export type response<T = any> = {
  data: T,
  errors?: any
}

export function GetAjaxObservable<T>(query: string, variables: {}, withCredentials = false, requestUrl = url, forGraphql = true) {

  return GetTokenObservable().pipe(
    mergeMap(() => {
      const token: TokenType = JSON.parse(getCookie("access_token")!)
      return ajax<response<T>>({
        url: requestUrl,
        method: "POST",
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token.value,
        },
        body: JSON.stringify({
          query,
          variables
        }),
        withCredentials: withCredentials
      })
    }),
    catchError((error) => {

      throw "error"
    }
    )
  )
}

export function GetTokenObservable() {
  return new Observable<void>((subscriber) => {
    const sub = timer(10, 20).subscribe({
      next: () => {
        let refreshSentString = getCookie("refresh_sent");
        let isTokenSent: boolean = refreshSentString ? JSON.parse(refreshSentString) : refreshSentString

        if (!isTokenSent) {
          subscriber.next()
          sub.unsubscribe()
        }
      }
    })
  })
}