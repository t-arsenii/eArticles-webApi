import { Box, Container, Typography } from "@mui/material"

type Props = {}

function Footer({ }: Props) {
  return (
    <>
      <Box sx={{backgroundColor:"#16191c", height:"100px", marginTop:"20px", display: "flex", alignItems: "center"}}>
        <Container sx={{color:"white", width:"50%", textAlign:"center"}}>
          Footer
        </Container>
      </Box>
    </>
  )
}

export default Footer