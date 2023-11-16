import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { User } from "../../Types/User"

export interface AccountState {
    Account: User,
    GlobalError: String,
    LogInError: String
}
const initialState: AccountState = {
    Account: {} as User,
    GlobalError: '',
    LogInError: ''
};

export const AccountSlice = createSlice({
    name: "Account",
    initialState,
    reducers: {
        getAccount: (state,
            action: PayloadAction<User>) => {
            state.Account = action.payload;
        },
        setLogInError: (state,
            action: PayloadAction<String>) => {
            state.LogInError = action.payload;
        },
        setGlobalError: (state,
            action: PayloadAction<String>) => {
            state.GlobalError = action.payload;
        }
    }
});

export const {
    getAccount,
    setLogInError,
    setGlobalError
} = AccountSlice.actions;
export default AccountSlice.reducer;