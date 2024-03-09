import { Box, Typography } from "@mui/material"
import ArticlePage from "../components/ArticlePage"

type Props = {}

function Latest({ }: Props) {
    return (
        <>
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>LATEST</Typography>
            </Box>
            <ArticlePage url={"http://localhost:5000/api/Articles"} />
        </>
    )
}

export default Latest