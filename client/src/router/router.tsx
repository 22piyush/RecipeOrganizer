import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import Auth from "../auth/Auth";
import Home from "../pages/home/Home";
import NotFound from "../components/NotFound";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/home",
        element: <Home />,
      },
    ],
  },
  {
    path: "/auth",
    element: <Auth />,
  },
  {
    path: "*",
    element: <NotFound />,
  }
]);

export default router;
