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
  dateFilter,
} from "react-bootstrap-table2-filter";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";

import _ from "lodash";
import axios from "axios";
import moment from "moment";

const ApiUrl = "http://localhost:5002/api/products";

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

      var fts = [];

      for (const dataField in filters) {
        const { filterVal, filterType, comparator } = filters[dataField];
        if (filterType === "TEXT") {
          fts.push({
            fieldName: _.startCase(_.toLower(dataField)),
            comparision: "Contains",
            fieldValue: filterVal,
          });
        } else if (filterType === "NUMBER" && filterVal.number != null) {
          if (!filterVal.comparator == "") {
            fts.push({
              fieldName: _.startCase(_.toLower(dataField)),
              comparision:
                filterVal.comparator === "=" ? "==" : filterVal.comparator,
              fieldValue: filterVal.number,
            });
          }
        } else if (filterType === "DATE" && filterVal.date != null) {
          if (!(filterVal.comparator == "" || filterVal.comparator == null)) {
            fts.push({
              fieldName: _.startCase(_.toLower(dataField)),
              comparision:
                filterVal.comparator === "=" ? "==" : filterVal.comparator,
              fieldValue: filterVal.date,
            });
          }
        }
      }

      if (fts.length > 0) {
        queryModel = _.assign(queryModel, {
          filters: fts,
        });
      }

      axios.post(ApiUrl, queryModel).then((response) => {
        setProducts(response.data.items);
        setPage(response.data.page);
        setSizePerPage(response.data.pageSize);
        setTotalSize(response.data.totalItems);
      });
    },
    []
  );

  const rowSelect = (row, isSelect) => {
    setSelectedProduct(row);
  };

  const handleEdit = () => {
    console.log(selectedProduct);
  };

  const handleDelete = () => {
    console.log(selectedProduct);
  };

  const handleClearFilters = () => {
    if (nameFilter != undefined || _.isFunction(nameFilter)) {
      nameFilter("");
    }
    if (priceFilter != undefined || _.isFunction(priceFilter)) {
      priceFilter("");
    }
    if (quantityFilter != undefined || _.isFunction(quantityFilter)) {
      quantityFilter("");
    }
    if (createdFilter != undefined || _.isFunction(createdFilter)) {
      createdFilter();
    }
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
              <Button variant="primary">
                <FontAwesomeIcon icon={faPlus} /> Create
              </Button>
              <Button variant="warning" onClick={handleEdit}>
                <FontAwesomeIcon icon={faEdit} /> Update
              </Button>
              <Button variant="danger" onClick={handleDelete}>
                <FontAwesomeIcon icon={faTrash} /> Delete
              </Button>
              <DropdownButton
                as={ButtonGroup}
                id="bg-nested-dropdown"
                variant="outline-secondary"
                title=""
              >
                <Dropdown.Item eventKey="1" onClick={handleClearFilters}>
                  Clear filters
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

let nameFilter;
let priceFilter;
let quantityFilter;
let createdFilter;

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
      getFilter: (filter) => {
        nameFilter = filter;
      },
    }),
    sort: true,
  },
  {
    dataField: "cost",
    text: "Product price",
    filter: numberFilter({
      getFilter: (filter) => {
        priceFilter = filter;
      },
    }),
    sort: true,
  },
  {
    dataField: "quantity",
    text: "Quantity",
    filter: numberFilter({
      getFilter: (filter) => {
        quantityFilter = filter;
      },
    }),
    sort: true,
  },
  {
    dataField: "created",
    text: "Created",
    formatter: (cell, row) => {
      return moment(cell).format("l");
    },
    filter: dateFilter({
      getFilter: (filter) => {
        createdFilter = filter;
      },
    }),
    sort: true,
  },
  {
    dataField: "productCodeName",
    text: "Product Code",
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
