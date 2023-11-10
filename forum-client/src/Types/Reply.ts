export interface Reply{
    id: Number,
    text: string,
    date: Date,
    comment_Id: Number,
    reply_Id: Number
    user_Id: Number,
    user_Username: string,
    reply_Username: string
    likes: Number,
    liked: boolean,
}
export interface ReplyInput {
    text: string,
    user_Id: Number,
    post_Id: Number,
    reply_Id?: Number,
}