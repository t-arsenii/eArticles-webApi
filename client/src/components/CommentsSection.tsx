import React, { useState } from 'react';
import {
    Avatar,
    Button,
    Card,
    CardContent,
    Grid,
    TextField,
    Typography,
} from '@mui/material';

interface Comment {
    id: number;
    text: string;
    parentId?: number;
}

export function CommentsSection() {
    const [comments, setComments] = useState<Comment[]>([]);
    const [newComment, setNewComment] = useState('');

    const handleCommentChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setNewComment(event.target.value);
    };

    const handleCommentSubmit = () => {
        const newCommentObject: Comment = {
            id: comments.length + 1,
            text: newComment,
        };

        setComments([...comments, newCommentObject]);
        setNewComment('');
    };

    return (
        <div>
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        label="Write a comment"
                        variant="outlined"
                        value={newComment}
                        onChange={handleCommentChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button variant="contained" color="primary" onClick={handleCommentSubmit}>
                        Submit
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    {comments.map((comment) => (
                        <Card key={comment.id} style={{ marginBottom: '10px' }}>
                            <CardContent>
                                <Grid container spacing={2} alignItems="center">
                                    <Grid item>
                                        <Avatar>{comment.text.charAt(0)}</Avatar>
                                    </Grid>
                                    <Grid item>
                                        <Typography>{comment.text}</Typography>
                                    </Grid>
                                </Grid>
                            </CardContent>
                        </Card>
                    ))}
                </Grid>
            </Grid>
        </div>
    );
};
