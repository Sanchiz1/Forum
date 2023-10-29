import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, delay, map, mergeMap, Observable, of, timer } from "rxjs";
import { redirect } from "react-router-dom";
import { GetAjaxObservable, TokenType } from "./loginRequests";
import { getCookie } from "../Helpers/CookieHelper";
import { User, UserInput } from "../Types/User";
import { Post } from "../Types/Post";

const url = "https://localhost:7295/graphql";

interface GraphqlPosts{
    data:{
        posts:{
            posts: Post[]
        }
    }
}

export function requestPosts() {
    return ajax<GraphqlPosts>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `query{
              posts{
                posts{
                    id,
                    title,
                    text,
                    date,
                    user_Id
                }
              }
            }`
        }),
        withCredentials: true,
    }).pipe(
        map((value) => {
            return value.response.data.posts.posts;
        }),
        catchError((error) => {
            throw error
        })
    );
}