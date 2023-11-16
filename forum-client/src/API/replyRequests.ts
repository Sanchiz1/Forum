import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, map } from "rxjs";
import { Reply, ReplyInput } from "../Types/Reply";
import { GetAjaxObservable } from "./loginRequests";

const url = "https://localhost:7295/graphql";

interface GraphqlReply {
    data: {
        replies: {
            replies: Reply[]
        }
    }
}

export function requestReplies(comment_id: Number, offset: Number, next: Number, order: String, user_timestamp: Date) {
    return ajax<GraphqlReply>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `
            query($Input:  GetRepliesInput!){
                replies{
                    replies(input: $Input){
                        id
                        text
                        date
                        comment_Id
                        reply_Id
                        user_Id
                        user_Username
                        reply_Username
                        likes
                        liked
                    }
                }
            }`,
            variables: {

                "Input": {
                    "comment_Id": comment_id,
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
            return value.response.data.replies.replies;
        }),
        catchError((error) => {
            throw error
        })
    );
}

interface GraphqlCreateReply {
    reply: {
        createReply: string
    }
}

export function createReplyRequest(ReplyInput: ReplyInput) {
    return GetAjaxObservable<GraphqlCreateReply>(`
        mutation($Input: CreateReplyInput!){
            reply{
              createReply(input: $Input)
            }
          }`,
        {
            "Input": {
                "comment_Id": ReplyInput.comment_Id,
                "text": ReplyInput.text,
                "user_Id": ReplyInput.user_Id,
                "reply_Id": ReplyInput.reply_Id
            }
        }
    ).pipe(
        map((value) => {

            if (value.response.errors) {

                throw new Error(value.response.errors[0].message);
            }

            return value.response.data.reply.createReply;

        }),
        catchError((error) => {
            throw error
        })
    );
}