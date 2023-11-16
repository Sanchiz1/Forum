import { useState } from 'react';
import { Button, Box, TextField } from "@mui/material";
import { SxProps, Theme } from "@mui/material/styles";
import { OverridableStringUnion } from '@mui/types';
import { useDispatch } from "react-redux";
import { isSigned } from "../../API/loginRequests";
import { setLogInError } from "../../Redux/Reducers/AccountReducer";

interface CommentInputProps {
    Action: (e : string) => void,
    sx?: SxProps<Theme> | undefined,
}

export default function CommentInputElement(Props: CommentInputProps) {
    const dispatch = useDispatch();
    const [comment, setComment] = useState('');
    const [focuse, setFocuse] = useState(false);


    const handlesubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        Props.Action(comment!.toString())
        setComment('');
    }
    return (
        <Box component="form"
            noValidate sx={{ m: 0 }}
            onSubmit={handlesubmit}
        >
            <TextField
                variant="standard"
                placeholder='Add a comment'
                name="comment"
                required
                fullWidth
                multiline
                minRows={1}
                sx={{ mb: 2 }}
                onFocus={(e) => {
                    if (!isSigned()) {
                        e.currentTarget?.blur();
                        dispatch(setLogInError('Not signed in'));
                    }
                    else {
                        setFocuse(true);
                    }
                }
                }

                value={comment}
                onChange={(e) => setComment(e.target.value)}
            />
            {focuse ?
                <Box sx={{ display: 'flex' }}>
                    <Button
                        color='secondary'
                        sx={{ ml: 'auto', mr: 1 }}
                        variant="text"
                        onClick={() => { setFocuse(false); setComment('') }}
                    >
                        Cancel
                    </Button>
                    <Button
                        type="submit"
                        variant="text"
                    >
                        Submit
                    </Button>
                </Box>
                :
                <></>}
        </Box>
    )
}