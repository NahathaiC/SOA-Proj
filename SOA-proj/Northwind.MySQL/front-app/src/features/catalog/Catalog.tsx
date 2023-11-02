import { Box, Typography, Paper, Table, TableHead, TableRow, TableCell, TableBody, TableContainer, Button, Link,} from "@mui/material";
import FindInPageIcon from '@mui/icons-material/FindInPage';

export default function Catalog( props: any) {
  return (
    <>
      <Box display="flex" justifyContent="space-between">
        <Typography sx={{ p: 2 }} variant="h4">
          Northwind Final Project
        </Typography>
      </Box>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell align="center">Product ID</TableCell>
              <TableCell align="center">Product Name</TableCell>
              <TableCell align="center">QuantityPerUnit</TableCell>
              <TableCell align="center">UnitsInStock</TableCell>
              <TableCell align="center">UnitsOnOrder</TableCell>
              <TableCell align="center">ReorderLevel</TableCell>
              {/* <TableCell align="center">Discontinued</TableCell> */}
              <TableCell align="center">Category ID</TableCell>
              <TableCell align="center">Category Name</TableCell>
              <TableCell align="center">Supplier ID</TableCell>
              <TableCell align="center">Company Name</TableCell>
              <TableCell align="center">Edit</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.products.map((product: any) => {
              return (
                <TableRow
                  key={product.productId}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell component="th" scope="row" align="center">
                    {product.productId}
                  </TableCell>
                  <TableCell align="center">{product.productName}</TableCell>
                  <TableCell align="center">
                    {product.quantityPerUnit}
                  </TableCell>
                  <TableCell align="center">{product.unitsInStock}</TableCell>
                  <TableCell align="center">{product.unitsOnOrder}</TableCell>
                  <TableCell align="center">{product.reorderLevel}</TableCell>
                  {/* <TableCell align="center">
                    {product.discontinued}
                  </TableCell> */}
                  <TableCell align="center">
                    {product.category.categoryId}
                  </TableCell>
                  <TableCell align="center">
                    {product.category.categoryName}
                  </TableCell>
                  <TableCell align="center">
                    {product.supplier.supplierId}
                  </TableCell>
                  <TableCell align="center">
                    {product.supplier.companyName}
                  </TableCell>

                  <TableCell align="center">
                    <Button
                      // component={Link}
                      // to={`/pr-catalog/${product.productId}`} // Update the path as needed
                      size="small"
                    >
                      <FindInPageIcon />
                    </Button>
                  </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
