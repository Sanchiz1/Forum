import { ajax } from "rxjs/internal/ajax/ajax";
import { catchError, map } from "rxjs";
import { Comment } from "../Types/Comment";

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
            query($Post_Id: Int!, $Offset: Int!, $Next: Int!, $Order: String!, $User_timestamp: DateTime!){
                comments{
                    comments(post_id: $Post_Id, offset: $Offset, next: $Next, order: $Order, user_timestamp: $User_timestamp){
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
                "Post_Id": post_Id,
                "Offset": offset,
                "Next": next,
                "Order": order,
                "User_timestamp": user_timestamp.toISOString()
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