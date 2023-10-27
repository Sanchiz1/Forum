export interface User {
    id: number,
    username: string,
    email: string,
    bio: string,
    registered_At: Date
}
export interface UserRegistration {
    id: number,
    username: string,
    email: string,
    bio: string,
    password: string
}

export interface UserInput {
    username: string,
    email: string,
    bio?: string,
    password: string
}