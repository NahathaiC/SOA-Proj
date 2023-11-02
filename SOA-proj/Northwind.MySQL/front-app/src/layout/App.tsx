import { useEffect, useState } from "react";
import { Product } from "../models/product";
import Catalog from "../features/catalog/Catalog";


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
    <>
      <Catalog products={products} />
    </>
  );
}

export default App;
