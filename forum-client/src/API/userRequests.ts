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
          role
          role_Id
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
    users: {
        user: User
    }
}

export function requestUserById(id: Number) {
    return GetAjaxObservable<GraphqlUser>(`
    query($Input:  GetRepliesInput!){
        user:userById(input: $Input){
          id
          username
          email
          bio
          registered_At
          role
          role_Id
      }
    }`, {
        "Input": {
            "id": id
        }
    }
    ).pipe(
        map(res => {
            return res.response.data.users.user;
        }),
        catchError((error) => {
            throw error;
        })
    );
}

export function requestUserByUsername(usernaame: string) {
    return GetAjaxObservable<GraphqlUser>(`
    query($Input:  GetUserByUsernameInput!){
        users{
            user:userByUsername(input: $Input){
            id
            username
            email
            bio
            registered_At
            role
            role_Id
            }
        }
    }`, {
        "Input": {
            "username": usernaame
        }
    },
        false
    ).pipe(
        map(res => {
            return res.response.data.users.user;
        }),
        catchError((error) => {
            throw error;
        })
    );
}

export function requestAccount() {
    return GetAjaxObservable<GraphqlUser>(`query{
        users{
            user:account{
                id
                username
                email
                bio
                registered_At
                role
                role_Id
        }
      }
    }`, {}).pipe(
        map(res => {
            return res.response.data.users.user;
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
        mutation($Input: CreateUserInput!){
            user{
              createUser(input: $Input)
            }
          }`,
            variables: {
                "Input": {
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

interface GraphqlUpdateUser {
    user: {
        user: string
    }
}

export function updateUserRequest(UserInput: UserInput) {
    return GetAjaxObservable<GraphqlUpdateUser>(`
        mutation($Input: UpdateUserInput!){
            user{
              user:updateUser(input: $Input)
            }
          }`,
        {
            "Input": {
                "username": UserInput.username,
                "email": UserInput.email,
                "bio": UserInput.bio
            }
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.user.user;

        }),
        catchError((error) => {
            throw error
        })
    );
}

export function changeUserRoleRequest(UserInput: UserInput) {
    return GetAjaxObservable<GraphqlUpdateUser>(`
        mutation($Input: UpdateUserInput!){
            user{
              user:updateUser(input: $Input)
            }
          }`,
        {
            "Input": {
                "username": UserInput.username,
                "email": UserInput.email,
                "bio": UserInput.bio
            }
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.user.user;

        }),
        catchError((error) => {
            throw error
        })
    );
}