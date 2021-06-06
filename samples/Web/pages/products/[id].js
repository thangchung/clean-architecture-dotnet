import { useCallback, useState, useEffect } from "react";
import { useRouter } from "next/router";

import { Button } from "antd";

import axios from "axios";

import MainLayout from "../../layout/MainLayout";
import HeadDefault from "../../layout/head/HeadDefault";

const ApiUrl = `http://localhost:5002/api/products`;

const Product = () => {
  const router = useRouter();
  const { id } = router.query;

  const [isloaded, setIsloaded] = useState(false);
  const [product, setProduct] = useState({});

  const fetchData = useCallback(
    async (id) => {
      axios.get(`${ApiUrl}/${id}`).then((response) => {
        setProduct(response.data.data);
        setIsloaded(true);
      });
    },
    [id]
  );

  useEffect(() => {
    fetchData(id);
  }, [fetchData]);

  return (
    <>
      <HeadDefault
        title="Product Detail page | eCommerce"
        description="Product detail page of eCommerce."
      />
      <MainLayout>
        <h1>Product Information</h1>
        {!isloaded ? (
          <div>Loading...</div>
        ) : (
          <>
            <p>Product name: {product.name}</p>
            <p>Product code: {product.productCodeName}</p>
            <p>Quantity: {product.quantity}</p>
            <p>Price: {product.cost}</p>
            <p>Active: {product.active.toString()}</p>
          </>
        )}
        <Button type="link" onClick={() => router.back()}>
          Back
        </Button>
      </MainLayout>
    </>
  );
};

export default Product;
