import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div className="flex min-h-screen items-center justify-center bg-white px-6">
      <div className="text-center">
        <h1 className="text-9xl font-extrabold text-gray-200">404</h1>

        <h2 className="mt-4 text-4xl font-bold text-gray-900">
          Page Not Found
        </h2>

        <p className="mt-4 text-lg text-gray-500">
          Sorry, the page you are looking for doesn't exist or has been moved.
        </p>

        <Link
          to="/home"
          className="mt-8 inline-block rounded-lg bg-black px-6 py-3 text-white transition hover:bg-gray-800"
        >
          Back to Home
        </Link>
      </div>
    </div>
  );
}