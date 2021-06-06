import React, { useCallback, useState, useEffect } from "react";
import { useRouter } from "next/router";

import MainLayout from "../../layout/MainLayout";
import HeadDefault from "../../layout/head/HeadDefault";

import { Row, Col, Button, Popconfirm } from "antd";

import _ from "lodash";
import axios from "axios";
import { atom, useAtom } from "jotai";

import List from "./components/List";
import Filter from "./components/Filter";

const ApiUrl = "http://localhost:5002/api/products-query";

const pageAtom = atom(1);
const pageSizeAtom = atom(10);
const totalSizeAtom = atom(0);

const filterAtom = atom({});
const sorterAtom = atom({});

const Products = () => {
  const router = useRouter();
  const [products, setProducts] = useState([]);

  const [page, setPage] = useAtom(pageAtom);
  const [pageSize, setPageSize] = useAtom(pageSizeAtom);
  const [totalSize, setTotalSize] = useAtom(totalSizeAtom);

  const [filter, setFilter] = useAtom(filterAtom);
  const [sorter, setSorter] = useAtom(sorterAtom);

  const [selectedRowKeys, setSelectedRowKeys] = useState([]);

  const fetchData = useCallback(async (page, pageSize, filter, sorter) => {
    var queryModel = {
      includes: ["Returns", "Code"],
    };

    queryModel = _.assign(queryModel, {
      page: page,
      pageSize: pageSize,
    });

    if (!_.isNil(sorter && sorter.field && sorter.order)) {
      queryModel = _.assign(queryModel, {
        sorts:
          (sorter == sorter.order) === "ascend"
            ? [`${sorter.field}`]
            : [`${sorter.field}Desc`],
      });
    }

    if (!_.isNil(filter)) {
      var fts = [];
      Object.keys(filter).map(function (fieldName, _) {
        if (filter[fieldName]) {
          if (fieldName == "name") {
            fts.push({
              fieldName: fieldName,
              comparision: "Contains",
              fieldValue: filter[fieldName],
            });
          } else if (fieldName == "quantity") {
            fts.push({
              fieldName: fieldName,
              comparision: "<=",
              fieldValue: `${filter[fieldName]}`,
            });
          }
        }
      });

      if (fts.length > 0) {
        queryModel = _.assign(queryModel, {
          filters: fts,
        });
      }
    }

    console.log("query-model:", queryModel);
    axios.post(ApiUrl, queryModel).then((response) => {
      setProducts(response.data.data.items);
      setPage(response.data.data.page);
      setPageSize(response.data.data.pageSize);
      setTotalSize(response.data.data.totalItems);
    });
  }, []);

  useEffect(() => {
    fetchData(page, pageSize, filter, sorter);
  }, [fetchData]);

  const onChange = (pagination, _, sort, extra) => {
    if (extra.action == "paginate") {
      setPage(pagination.current);
      setPageSize(pagination.pageSize);
    }

    if (extra.action == "sort") {
      setSorter({
        field: sort.field,
        order: sort.order,
      });
    }

    fetchData(pagination.current, pagination.pageSize, filter, {
      field: sort.field,
      order: sort.order,
    });
  };

  const rowSelection = {
    onChange: (selectedRowKeys, _) => {
      setSelectedRowKeys(selectedRowKeys);
    },
    selectedRowKeys: selectedRowKeys,
    getCheckboxProps: (record) => ({
      name: record.name,
    }),
  };

  const handleRefresh = () => {
    router.push("/products");
  };

  const handleDeleteItems = () => {
    setSelectedRowKeys([]);

    // do delete items

    handleRefresh();
  };

  const filterProps = {
    filter: filter,
    onFilterChange: (fields) => {
      setFilter(fields);
      fetchData(page, pageSize, filter, sorter);
    },
    onAdd: () => {
      router.push(`products/create`);
    },
  };

  return (
    <>
      <HeadDefault
        title="Product Page | eCommerce"
        description="Product page of eCommerce."
      />
      <MainLayout>
        <h1>Product Page</h1>
        <br></br>
        <Filter {...filterProps} />
        {selectedRowKeys.length > 0 && (
          <Row style={{ marginBottom: 24, textAlign: "right", fontSize: 13 }}>
            <Col>
              {`Selected ${selectedRowKeys.length} items `}
              <Popconfirm
                title="Are you sure delete these items?"
                placement="left"
                onConfirm={handleDeleteItems}
              >
                <Button type="primary" style={{ marginLeft: 8 }}>
                  Remove
                </Button>
              </Popconfirm>
            </Col>
          </Row>
        )}
        <List
          products={products}
          pagination={{
            current: page,
            pageSize: pageSize,
            total: totalSize,
            showTotal: (total) => `Total ${total} Items`,
          }}
          onChange={onChange}
          rowSelection={{
            type: "checkbox",
            ...rowSelection,
          }}
          onEditItem={(item) => {
            console.log(item);
            router.push(`products/${item.id}`);
          }}
          onDeleteItem={() => {
            handleRefresh();
          }}
        ></List>
      </MainLayout>
    </>
  );
};

export default Products;
