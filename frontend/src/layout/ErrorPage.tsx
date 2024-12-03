import { useRouteError } from 'react-router-dom';
import Button from '../components/Button';

export default function ErrorPage() {
  const routeError: unknown = useRouteError();
  const error = routeError as { status: number };
  return (
    <div className='bg-gray-800 flex h-screen w-screen items-start justify-center'>
      <div className='bg-danger text-primary mt-20 rounded-md p-4 text-lg'>
        {error.status === 404 ? 'Sorry, the page you are looking for does not exist.' : 'An error occurred'}

        <Button onClick={() => window.history.back()} buttonStyle='primary' className='mt-4 block'>
          Go Back
        </Button>
      </div>
    </div>
  );
}
