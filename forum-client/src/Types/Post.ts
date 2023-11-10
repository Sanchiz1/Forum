export interface Post{
    id: Number,
    title: string,
    text?: string,
    date: Date,
    user_Id: Number
    user_Username: string,
    likes: Number,
    comments: Number,
    liked: boolean
}

export interface PostInput {
    title: string,
    text?: string,
    user_Id: Number
}