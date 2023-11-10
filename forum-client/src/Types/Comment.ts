export interface Comment{
    id: Number,
    text: string,
    date: Date,
    post_Id: Number
    user_Id: Number
    user_Username: string,
    likes: Number,
    replies: Number,
    liked: boolean
}
