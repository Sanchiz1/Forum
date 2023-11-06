import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, map } from "rxjs";
import { Reply } from "../Types/Reply";

const url = "https://localhost:7295/graphql";

interface GraphqlReply {
    data: {
        replies: {
            replies: Reply[]
        }
    }
}

export function requestReplies(post_Id: Number) {
    return ajax<GraphqlReply>({
        url: url,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
        body: JSON.stringify({
            query: `
            query($id: Int!){
                replies{
                    replies(post_Id: $id){
                        id
                        text
                        date
                        post_Id
                        reply_Id
                        user_Id
                        user_Username
                    }
                }
            }`,
            variables: {
                "id": post_Id
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