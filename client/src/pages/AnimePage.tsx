import { Box, Typography } from "@mui/material"
import ArticlePage from "../components/ArticlePage"

type Props = {}

function AnimePage({ }: Props) {
    return (
        <>
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>Anime</Typography>
            </Box>
            <ArticlePage url={"http://localhost:5000/api/Articles/"} category={"Anime"} />
        </>
    )
}

export default AnimePage