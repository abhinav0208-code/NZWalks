import {
  Button,
  Container,
  Dialog,
  DialogContent,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TextField,
  Toolbar,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import {
  CreateRegion,
  DeleteRegion,
  GetAllRegions,
} from "../Services/RegionService";
import { AddRegionDialog } from "../Components/AddRegionComponent";

export const RegionContainer = () => {
  const [regions, setRegions] = useState(undefined);
  const [image, setImage] = useState("");
  const [openAddDialog, setOpenAddDialog] = useState(false);

  async function GetAll() {
    const response = await GetAllRegions();
    const regions = await response.json();
    setRegions(regions);
  }

  //delete a particular region
  const onDeleteClick = async (id) => {
    const response = await DeleteRegion(id);
    if (response.status == 200) {
      GetAll();
    }
  };

  const addRegion = async ({ code, name, regionImageUrl }) => {
    const response = await CreateRegion({ code, name, regionImageUrl });
    if (response.status === 201) {
      GetAll();
      setOpenAddDialog(false);
    }
  };

  //get the initial data on component render.
  useEffect(() => {
    GetAll();
  }, []);

  return regions === undefined ? null : (
    <Container sx={{ mt: 4 }}>
      <Toolbar>
        <Typography variant="h6">Regions</Typography>

        <Button variant="contained" onClick={() => setOpenAddDialog(true)}>
          Add
        </Button>
      </Toolbar>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Code</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Image</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {regions.map((r) => (
            <TableRow key={r.code}>
              <TableCell>{r.code}</TableCell>
              <TableCell>{r.name}</TableCell>
              <TableCell>
                <Button
                  variant="outlined"
                  size="small"
                  onClick={() => setImage(r.regionImageUrl)}
                >
                  View
                </Button>
              </TableCell>
              <TableCell>
                <Button
                  variant="contained"
                  onClick={(id) => onDeleteClick(r.id)}
                >
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <Dialog open={!!image} onClose={() => setImage("")}>
        <DialogContent>
          <img src={image} alt="region" style={{ width: "100%" }} />
        </DialogContent>
      </Dialog>

      <AddRegionDialog
        isOpen={openAddDialog}
        setIsOpen={setOpenAddDialog}
        addRegion={addRegion}
      />
    </Container>
  );
};
