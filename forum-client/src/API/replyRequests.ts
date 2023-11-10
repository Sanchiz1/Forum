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
            query($Comment_id: Int!, $Offset: Int!, $Next: Int!, $Order: String!, $User_timestamp: DateTime!){
                replies{
                    replies(comment_id: $Comment_id, offset: $Offset, next: $Next, order: $Order, user_timestamp: $User_timestamp){
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
                "Comment_id": comment_id,
                "Offset": offset,
                "Next": next,
                "Order": order,
                "User_timestamp": user_timestamp.toISOString()
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