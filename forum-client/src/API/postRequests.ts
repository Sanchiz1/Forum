import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, delay, map, mergeMap, Observable, of, timer } from "rxjs";
import { redirect } from "react-router-dom";
import { GetAjaxObservable, TokenType } from "./loginRequests";
import { getCookie } from "../Helpers/CookieHelper";
import { User, UserInput } from "../Types/User";
import { Post, PostInput } from "../Types/Post";

const url = "https://localhost:7295/graphql";

interface GraphqlPosts {
    data: {
        posts: {
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
                    user_Id,
                    user_Username
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

export function requestPagedPosts(offset: Number, next: Number, order: String) {
    return ajax<GraphqlPosts>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `query($Offset: Int!, $Next: Int!, $Order: String!){
              posts{
                posts:pagedPosts(offset: $Offset, next: $Next, order: $Order){
                    id,
                    title,
                    text,
                    date,
                    user_Id,
                    user_Username
                }
              }
            }`,
            variables: {
                "Offset": offset,
                "Next": next,
                "Order": order
            }
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

interface GraphqlCreatePost {
    post: {
        createPost: string
    }
}

export function createPostRequest(PostInput: PostInput) {
    return GetAjaxObservable<GraphqlCreatePost>(`
        mutation($Post: PostInput!){
            post{
              createPost(post: $Post)
            }
          }`,
        {
            "Post": {
                "title": PostInput.title,
                "text": PostInput.text,
                "user_Id": PostInput.user_Id
            }
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.post.createPost;

        }),
        catchError((error) => {
            throw error
        })
    );
}

interface GraphqlUpdatePost {
    post: {
        updatePost: string
    }
}

export function updatePostRequest(text: String, id : Number) {
    return GetAjaxObservable<GraphqlUpdatePost>(`
        mutation($text: String!, $id: Int!){
            post{
              updatePost(text: $text, id: $id)
            }
          }`,
        {
            "text": text,
            "id": id
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.post.updatePost;

        }),
        catchError((error) => {
            throw error
        })
    );
}


interface GraphqlDeletePost {
    post: {
        deletePost: string
    }
}

export function deletePostRequest( id : Number) {
    return GetAjaxObservable<GraphqlDeletePost>(`
        mutation($id: Int!){
            post{
              deletePost( id: $id)
            }
          }`,
        {
            "id": id
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.post.deletePost;

        }),
        catchError((error) => {
            throw error
        })
    );
}


interface GraphqlPost {
    data: {
        posts: {
            post: Post
        }
    }
}

export function requestPostById(id: Number) {
    return ajax<GraphqlPost>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `query($id: Int!){
              posts{
                post(id: $id){
                    id,
                    title,
                    text,
                    date,
                    user_Id,
                    user_Username
                }
              }
            }`
            ,
            variables: {
                "id": id
            }
        }),
        withCredentials: true,
    }).pipe(
        map((value) => {
            return value.response.data.posts.post;
        }),
        catchError((error) => {
            throw error
        })
    );
}