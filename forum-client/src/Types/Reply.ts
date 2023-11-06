export interface Reply{
    id: Number,
    text: string,
    date: Date,
    user_Id: Number,
    post_Id: Number,
    reply_Id?: Number,
    user_Username: string,
}

export interface ReplyInput {
    text: string,
    user_Id: Number,
    post_Id: Number,
    reply_Id?: Number,
}