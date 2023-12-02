import { useState, useEffect } from 'react';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import {
    Box, Button, Container, CssBaseline, Paper, Link, IconButton,
    Dialog, DialogTitle, DialogActions, MenuItem, Tooltip, TextField,
    FormControl, InputLabel, Select, OutlinedInput, Chip, SelectChangeEvent
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { StyledMenu } from '../UtilComponents/StyledMenu';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';
import { Reply, ReplyInput } from '../../Types/Reply';
import { Comment } from '../../Types/Comment';
import { createReplyRequest, requestReplies } from '../../API/replyRequests';
import ButtonWithCheck from '../UtilComponents/ButtonWithCheck';
import { isSigned } from '../../API/loginRequests';
import CommentInputElement from '../UtilComponents/CommentInputElement';
import { enqueueSnackbar } from 'notistack';
import ReplyInputElement from '../UtilComponents/ReplyInputElement';
import { useDispatch, useSelector } from 'react-redux';
import { setGlobalError } from '../../Redux/Reducers/AccountReducer';
import { RootState } from '../../Redux/store';
import { deleteCommentRequest, likeCommentRequest, requestCommentById, updateCommentRequest } from '../../API/commentRequests';
import IconButtonWithCheck from '../UtilComponents/IconButtonWithCheck';
import { Category } from '../../Types/Category';
import { createCategoryRequest, requestCategories } from '../../API/categoryRequests';
import CategoryElement from './CategoryElement';


export default function CategoriesSelect() {
    const [categories, setCategories] = useState<Category[]>([])
    const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const Account = useSelector((state: RootState) => state.account.Account);

    const next = 4;
    const [fetching, setFetching] = useState(false);
    const [userTimestamp, setUserTimestamp] = useState(new Date());
    const [offset, setOffset] = useState(0);
    const [hasMore, setHasMore] = useState(true);

    const handleChange = (event: SelectChangeEvent<typeof selectedCategories>) => {
        const {
            target: { value },
        } = event;
        setSelectedCategories(typeof value === 'string' ? value.split(',') : value,);
    };

    const fetchCategories = () => {
        requestCategories(offset, next).subscribe({
            next(value) {
                if (value.length == 0) {
                    setHasMore(false);
                    return;
                }
                setCategories([...categories, ...value]);
                if (document.documentElement.offsetHeight - window.innerHeight < 100) {
                    setOffset(offset + next);
                }
            },
            error(err) {
                dispatch(setGlobalError(err.message));
            },
        })
    };

    useEffect(() => {
        fetchCategories()
        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, [offset])

    function handleScroll() {
        if (window.innerHeight + document.documentElement.scrollTop <= document.documentElement.scrollHeight - 10 || !hasMore) return;
        setOffset(offset + next);
    }

    return (
        <FormControl sx={{ mt: 1 }} fullWidth>
            <InputLabel id="demo-multiple-chip-label">Categories</InputLabel>
            <Select
                labelId="demo-multiple-chip-label"
                id="demo-multiple-chip"
                multiple
                value={selectedCategories}
                onChange={handleChange}
                input={<OutlinedInput id="select-multiple" label="Categories" />}
                renderValue={(selectedCategories) => (
                    <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                        {selectedCategories.map((category, index) => (
                            <Chip key={index} label={category} />
                        ))}
                    </Box>
                )}
            >
                {categories.map((category) => (
                    <MenuItem
                        key={category.id}
                        value={category.title}
                    >
                        {category.title}
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    )
}