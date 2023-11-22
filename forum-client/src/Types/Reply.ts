export interface Reply{
    id: Number,
    text: string,
    date_Created: Date,
    date_Edited?: Date,
    comment_Id: Number,
    reply_Id: Number
    user_Id: Number,
    user_Username: string,
    reply_Username: string
    likes: Number,
    liked: boolean,
    is_Deleted: boolean
}
export interface ReplyInput {
    text: string,
    user_Id: Number,
    comment_Id: Number,
    reply_Id?: Number,
}