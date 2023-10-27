import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, delay, map, mergeMap, Observable, of, timer } from "rxjs";
import { redirect } from "react-router-dom";
import { GetAjaxObservable, TokenType } from "./loginRequests";
import { getCookie } from "../Helpers/CookieHelper";
import { User, UserInput } from "../Types/User";

const url = "https://localhost:7295/graphql";


export function requestUsers(variables: {}) {
    return GetAjaxObservable<{}>(`query{
        users{
          id
          username
          email
          bio
          registered_At
      }
    }`, {}).pipe(
        map((value): string => {

            return "/";
        }),
        catchError((error) => {
            throw error
        })
    );
}

interface GraphqlUser {
    user: User
}

export function requestUserById(id: Number) {
    return GetAjaxObservable<GraphqlUser>(`query($id: Int!){
        user:userById(id: $id){
          id
          username
          email
          bio
          registered_At
      }
    }`, {
        "id": id
    }
    ).pipe(
        map(res => {
            return res.response.data.user;
        }),
        catchError((error) => {
            throw error;
        })
    );
}

export function requestUserByUsername(usernaame: string) {
    return GetAjaxObservable<GraphqlUser>(`query($usernaame: String!){
        user:userByUsername(username: $usernaame){
          id
          username
          email
          bio
          registered_At
      }
    }`, {
        "usernaame": usernaame
    },
        false
    ).pipe(
        map(res => {
            return res.response.data.user;
        }),
        catchError((error) => {
            throw error;
        })
    );
}

export function requestAccount() {
    return GetAjaxObservable<GraphqlUser>(`query{
        user:account{
          id
          username
          email
          bio
          registered_At
      }
    }`, {}).pipe(
        map(res => {
            return res.response.data.user;
        }),
        catchError((error) => {
            throw error;
        })
    );
}

interface GraphqlCreateUser {
    data: {
        user: {
            createUser: string
        }
    }
    errors: [
        {
            message: string
        }
    ]
}

export function createUserRequest(UserInput: UserInput) {
    return ajax<GraphqlCreateUser>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `
        mutation($User: UserInput!){
            user{
              createUser(user: $User)
            }
          }`,
            variables: {
                "User": {
                    "username": UserInput.username,
                    "email": UserInput.email,
                    "password": UserInput.password
                }
            }
        }),
        withCredentials: true,
    }).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.user.createUser;

        }),
        catchError((error) => {
            throw error
        })
    );
}