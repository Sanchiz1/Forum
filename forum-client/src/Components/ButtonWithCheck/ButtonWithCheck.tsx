import { Button, ButtonPropsVariantOverrides } from "@mui/material";
import { OverridableStringUnion } from '@mui/types';

interface ButtonProps {
    variant: OverridableStringUnion<'text' | 'outlined' | 'contained', ButtonPropsVariantOverrides>,
    ActionWithCheck: () => void
}

export default function ButtonWithCheck(Props: ButtonProps){


    return(
        <Button variant={Props.variant} onClick={Props.ActionWithCheck}>Create post</Button>
    )
}