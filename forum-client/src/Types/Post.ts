export interface Post{
    id: Number,
    title: string,
    text?: string,
    date: Date,
    user_Id: Number
    user_Username: string,
}

export interface PostInput {
    title: string,
    text?: string,
    user_Id: Number
}