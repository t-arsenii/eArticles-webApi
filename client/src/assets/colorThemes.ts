import { createTheme } from "@mui/material";

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#0D47A1', // Dark blue
    },
    secondary: {
      main: '#FF5722', // Deep orange
    },
    background: {
      default: '#121212', // Dark background
      paper: '#1E1E1E', // Slightly lighter dark for paper elements
    },
    text: {
      primary: '#E0E0E0', // Light grey for primary text
      secondary: '#B0B0B0', // Lighter grey for secondary text
    },
  },
  typography: {
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    h1: {
      fontSize: '2.5rem',
      fontWeight: 700,
      lineHeight: 1.2,
      color: '#E0E0E0',
    },
    h2: {
      fontSize: '2rem',
      fontWeight: 700,
      lineHeight: 1.3,
      color: '#E0E0E0',
    },
    h3: {
      fontSize: '1.75rem',
      fontWeight: 700,
      lineHeight: 1.4,
      color: '#E0E0E0',
    },
    h4: {
      fontSize: '1.5rem',
      fontWeight: 700,
      lineHeight: 1.5,
      color: '#E0E0E0',
    },
    body1: {
      fontSize: '1rem',
      lineHeight: 1.6,
      color: '#E0E0E0',
    },
    body2: {
      fontSize: '0.875rem',
      lineHeight: 1.6,
      color: '#B0B0B0',
    },
    button: {
      fontSize: '0.875rem',
      fontWeight: 600,
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: '8px',
        },
        containedPrimary: {
          backgroundColor: '#0D47A1',
          color: '#FFFFFF',
          '&:hover': {
            backgroundColor: '#083678',
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: '12px',
          boxShadow: '0 4px 8px rgba(0,0,0,0.3)', // Darker shadow for dark theme
        },
      },
    },
  },
});

const lightTheme = createTheme({
  palette: {
    primary: {
      main: '#0D47A1', // Dark blue
    },
    secondary: {
      main: '#FF5722', // Deep orange
    },
    background: {
      default: '#F4F4F4', // Light grey for background
      paper: '#FFFFFF', // White for paper elements
    },
    text: {
      primary: '#1c1c1c', // Dark blue for text
      secondary: '#4a4949', // Deep orange for secondary text
    },
  },
  typography: {
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    h1: {
      fontSize: '2.5rem',
      fontWeight: 700,
      lineHeight: 1.2,
    },
    h2: {
      fontSize: '2rem',
      fontWeight: 700,
      lineHeight: 1.3,
    },
    h3: {
      fontSize: '1.75rem',
      fontWeight: 700,
      lineHeight: 1.4,
    },
    h4: {
      fontSize: '1.5rem',
      fontWeight: 700,
      lineHeight: 1.5,
    },
    body1: {
      fontSize: '1rem',
      lineHeight: 1.6,
    },
    body2: {
      fontSize: '0.875rem',
      lineHeight: 1.6,
    },
    button: {
      fontSize: '0.875rem',
      fontWeight: 600,
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: '8px',
        },
        containedPrimary: {
          backgroundColor: '#0D47A1',
          color: '#FFFFFF',
          '&:hover': {
            backgroundColor: '#083678',
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: '12px',
          boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
        },
      },
    },
  },
});

export { darkTheme, lightTheme }