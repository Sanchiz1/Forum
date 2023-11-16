import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, map } from "rxjs";
import { Comment, CommentInput } from "../Types/Comment";
import { GetAjaxObservable } from "./loginRequests";

const url = "https://localhost:7295/graphql";

interface GraphqlComments {
    data: {
        comments: {
            comments: Comment[]
        }
    }
}

export function requestComments(post_Id: Number, offset: Number, next: Number, order: String, user_timestamp: Date) {
    return ajax<GraphqlComments>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `
            query($Input:  GetCommentsInput!){
                comments{
                    comments(input: $Input){
                        id
                        text
                        date
                        post_Id
                        user_Id
                        user_Username
                        likes
                        replies
                        liked
                    }
                }
            }`,
            variables: {
                "Input": {
                    "post_Id": post_Id,
                    "offset": offset,
                    "next": next,
                    "order": order,
                    "user_Timestamp": user_timestamp.toISOString()
                }
            }
        }),
        withCredentials: true,
    }).pipe(
        map((value) => {
            return value.response.data.comments.comments;
        }),
        catchError((error) => {
            throw error
        })
    );
}

interface GraphqlCommentById {
    data: {
        comments: {
            comment: Comment
        }
    }
}

export function requestCommentById(id: number) {
    return ajax<GraphqlCommentById>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `
            query($Input:  GetCommentByIdInput!){
                comments{
                    comment(input: $Input){
                        id
                        text
                        date
                        post_Id
                        user_Id
                        user_Username
                        likes
                        replies
                        liked
                    }
                }
            }`,
            variables: {
                "Input": {
                    "id": id
                }
            }
        }),
        withCredentials: true,
    }).pipe(
        map((value) => {
            return value.response.data.comments.comment;
        }),
        catchError((error) => {
            throw error
        })
    );
}

interface GraphqlCreatePost {
    comment: {
        createComment: string
    }
}

export function createCommentRequest(CommentInput: CommentInput) {
    return GetAjaxObservable<GraphqlCreatePost>(`
        mutation($Input: CreateCommentInput!){
            comment{
              createComment(input: $Input)
            }
          }`,
        {
            "Input": {
                "post_Id": CommentInput.post_Id,
                "text": CommentInput.text,
                "user_Id": CommentInput.user_Id
            }
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.comment.createComment;

        }),
        catchError((error) => {
            throw error
        })
    );
}