import React, { useCallback, useState } from "react";
import {
  Container,
  Card,
  ButtonGroup,
  Button,
  DropdownButton,
  Dropdown,
} from "react-bootstrap";

import BootstrapTable from "react-bootstrap-table-next";
import paginationFactory from "react-bootstrap-table2-paginator";
import filterFactory, {
  textFilter,
  numberFilter,
} from "react-bootstrap-table2-filter";

import _ from "lodash";
import axios from "axios";

const Products = (props) => {
  const [page, setPage] = useState(1);
  const [sizePerPage, setSizePerPage] = useState(10);
  const [products, setProducts] = useState([]);
  const [totalSize, setTotalSize] = useState(0);
  const [selectedProduct, setSelectedProduct] = useState({});

  const fetchData = useCallback(
    async (type, { page, sizePerPage, filters, sortField, sortOrder }) => {
      var queryModel = {
        sorts:
          sortOrder === "asc"
            ? [`${sortField}`]
            : [`${sortField}${_.startCase(_.toLower(sortOrder))}`],
        page: page,
        pageSize: sizePerPage,
      };

      for (const dataField in filters) {
        const { filterVal, filterType, comparator } = filters[dataField];

        if (filterType === "TEXT") {
          queryModel = _.assign(queryModel, {
            filters: [
              {
                fieldName: _.startCase(_.toLower(dataField)),
                comparision: "Contains",
                fieldValue: filterVal,
              },
            ],
          });
        }
      }

      axios
        .post("http://localhost:5002/api/products", queryModel)
        .then((response) => {
          setProducts(response.data.items);
          setPage(response.data.page);
          setSizePerPage(response.data.pageSize);
          setTotalSize(response.data.totalItems);
        });
    },
    []
  );

  const rowSelect = (row, isSelect) => {
    // console.log(row);
    setSelectedProduct(row);
  };

  const handleEdit = () => {
    console.log(selectedProduct);
  };

  const handleDelete = () => {
    console.log(selectedProduct);
  };

  const selectRow = {
    mode: "radio",
    clickToSelect: true,
    hideSelectAll: true,
    bgColor: "#6c757d",
    onSelect: rowSelect,
  };

  return (
    <>
      <br></br>
      <Container fluid>
        <Card>
          <Card.Header as="h3">
            <b>Product Management</b>
            <ButtonGroup aria-label="Basic example" className="float-right">
              <Button variant="primary">Create</Button>
              <Button variant="secondary" onClick={handleEdit}>
                Update
              </Button>
              <DropdownButton
                as={ButtonGroup}
                id="bg-nested-dropdown"
                variant="secondary"
              >
                <Dropdown.Item eventKey="1" onClick={handleDelete}>
                  Delete
                </Dropdown.Item>
                <Dropdown.Item eventKey="2">Export</Dropdown.Item>
              </DropdownButton>
            </ButtonGroup>
          </Card.Header>
          <Card.Body>
            <RemoteAll
              data={products}
              page={page}
              sizePerPage={sizePerPage}
              totalSize={totalSize}
              onTableChange={fetchData}
              selectRow={selectRow}
            />
          </Card.Body>
        </Card>
      </Container>
    </>
  );
};

const columns = [
  {
    dataField: "id",
    text: "Product id",
    hidden: true,
  },
  {
    dataField: "name",
    text: "Product name",
    filter: textFilter({
      defaultValue: "",
    }),
    sort: true,
  },
  {
    dataField: "cost",
    text: "Product price",
    filter: numberFilter(),
    sort: true,
  },
  {
    dataField: "productCodeName",
    text: "Product Code",
  },
  {
    dataField: "quantity",
    text: "Quantity",
    sort: true,
  },
];

const defaultSorted = [
  {
    dataField: "name",
    order: "desc",
  },
];

const RemoteAll = ({
  data,
  page,
  sizePerPage,
  onTableChange,
  selectRow,
  totalSize,
}) => (
  <BootstrapTable
    bootstrap4
    striped
    hover
    remote
    keyField="id"
    data={data}
    columns={columns}
    defaultSorted={defaultSorted}
    filter={filterFactory()}
    pagination={paginationFactory({ page, sizePerPage, totalSize })}
    onTableChange={onTableChange}
    selectRow={selectRow}
  />
);

export default Products;
