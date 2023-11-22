import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, delay, map, mergeMap, Observable, of, timer } from "rxjs";
import { redirect } from "react-router-dom";
import { GetAjaxObservable, TokenType } from "./loginRequests";
import { getCookie } from "../Helpers/CookieHelper";
import { User, UserInput } from "../Types/User";
import { Post, PostInput } from "../Types/Post";

const url = "https://localhost:7295/graphql";

interface GraphqlPosts {
    posts: {
        posts: Post[]
    }
}

export function requestPosts(offset: Number, next: Number, order: String, user_timestamp: Date) {
    return GetAjaxObservable<GraphqlPosts>(
        `query($Input:  GetPostsInput!){
              posts{
                posts(input: $Input){
                    id
                    title
                    text
                    date_Created
                    date_Edited
                    user_Id
                    user_Username
                    likes
                    comments
                    liked
                }
              }
            }`,
        {
            "Input": {
                "offset": offset,
                "next": next,
                "order": order,
                "user_Timestamp": user_timestamp.toISOString()
            }
        },
        false
    ).pipe(
        map((value) => {
            return value.response.data.posts.posts;
        }),
        catchError((error) => {
            throw error
        })
    );
}

export function requestUserPosts(author_username: String, offset: Number, next: Number, order: String, user_timestamp: Date) {
    return GetAjaxObservable<GraphqlPosts>(
        `query($Input: GetUserPostsInput!){
              posts{
                posts:userPosts(input: $Input){
                    id
                    title
                    text
                    date_Created
                    date_Edited
                    user_Id
                    user_Username
                    likes
                    comments
                    liked
                }
              }
            }`,
        {
            "Input": {
                "author_Username": author_username,
                "offset": offset,
                "next": next,
                "order": order,
                "user_Timestamp": user_timestamp.toISOString()
            }
        },
        false
    ).pipe(
        map((value) => {
            return value.response.data.posts.posts;
        }),
        catchError((error) => {
            throw error
        })
    );
}

interface GraphqlPost {
    posts: {
        post: Post
    }
}

export function requestPostById(id: Number) {
    return GetAjaxObservable<GraphqlPost>(
        `query($Input:  GetPostByIdInput!){
              posts{
                post(input: $Input){
                    id
                    title
                    text
                    date_Created
                    date_Edited
                    user_Id
                    user_Username
                    likes
                    comments
                    liked
                }
              }
            }`
        ,
        {
            "Input": {
                "id": id
            }
        },
        false
    ).pipe(
        map((value) => {
            return value.response.data.posts.post;
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
        mutation($Input: CreatePostInput!){
            post{
              createPost(input: $Input)
            }
          }`,
        {
            "Input": {
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

export function updatePostRequest(text: String, id: Number) {
    return GetAjaxObservable<GraphqlUpdatePost>(`
        mutation($Input: UpdatePostInput!){
            post{
              updatePost(input: $Input)
            }
          }`,
        {
            "Input": {
                "text": text,
                "id": id
            }
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

export function deletePostRequest(id: Number) {
    return GetAjaxObservable<GraphqlDeletePost>(`
        mutation($Input: DeletePostInput!){
            post{
              deletePost(input: $Input)
            }
          }`,
        {
            "Input": {
                "id": id
            }
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


interface GraphqlLikePost {
    post: {
        likePost: string
    }
}

export function likePostRequest(id: Number) {
    return GetAjaxObservable<GraphqlLikePost>(`
        mutation($Input: LikePostInput!){
            post{
              likePost(input: $Input)
            }
          }`,
        {
            "Input": {
                "post_Id": id
            }
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.post.likePost;

        }),
        catchError((error) => {
            throw error
        })
    );
}
