import { useEffect, useState } from "react";
import { Product } from "./product";

function App() {
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    fetch("http://localhost:5161/Products")
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data.$values)) {
          // Check if the array is available in the $values property
          setProducts(data.$values);
        } else {
          // If the array is directly in the data, use it
          setProducts(data);
        }
      })
      .catch((error) => console.error("Error fetching data:", error));
  }, []);

  if (products.length === 0) {
    return <p>Loading...</p>;
  }
  return (
    <div>
      <h1 style={{ color: "black" }}>Northwind Final Project</h1>
      <ul>
        {products.map((product) => (
          <li key={product.productId}>
            <strong>Product Id:</strong> {product.productId} - {product.productName}
            <br />
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
