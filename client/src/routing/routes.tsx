import { createBrowserRouter } from 'react-router-dom';
import ShipList from '../app/components/Ship/ShipList/ShipList';
import App from '../app/app';
import ErrorPage from '../app/components/ErrorPage';
import ShipForm from '../app/components/Ship/ShipForm/ShipForm';

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      { path: '', element: <ShipList /> },
      { path: 'ships/new', element: <ShipForm /> },
      { path: 'ships/:id', element: <ShipForm /> },
    ],
  },
]);

export default router;
