import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, map, mergeMap, Observable, of, timer } from "rxjs";
import { redirect } from "react-router-dom";
import { getCookie, TokenType } from "./login";

const url = "https://localhost:7295/graphql";


export function requestUsers(variables: {}) {
    
    const token: TokenType = JSON.parse(getCookie("access_token")!)
    return ajax<{}>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            'Authorization': 'Bearer ' + token.value,
        },
        body: JSON.stringify({
            query: `query{
        users{
          id
          username
          email
          bio
          registered_At
      }
    }`,
            variables
        }),
        withCredentials: false,
    }).pipe(
        map((value): string => {

            return "/";
        }),
        catchError((error) => {
            throw error
        })
    );
}
