import { Typography } from "@mui/material";
import Box from "@mui/material/Box";
import ArticlePage from "../components/ArticlePage";

export default function NewsPage() {
    return (
        <>
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>NEWS</Typography>
            </Box>
            <ArticlePage url={"http://localhost:5000/api/Articles"} articleType="news"/>
        </>
    )
}