import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  TextField,
} from "@mui/material";
import { useState } from "react";

export const AddRegionDialog = ({ isOpen, setIsOpen, addRegion }) => {
  const [code, setCode] = useState("");
  const [name, setName] = useState("");
  const [imageUrl, setImageUrl] = useState("");

  const onAddRegionClick = async () => {
    await addRegion({
      code: code,
      name: name,
      regionImageUrl: imageUrl,
    });
  };

  return (
    <Dialog open={isOpen} onClose={() => setIsOpen(false)}>
      <DialogContent>
        <TextField
          label={"Code"}
          onChange={(e) => setCode(e.target.value)}
        ></TextField>
        <TextField
          label={"Name"}
          onChange={(e) => setName(e.target.value)}
        ></TextField>

        <TextField
          label={"Image URL"}
          onChange={(e) => setImageUrl(e.target.value)}
        ></TextField>
      </DialogContent>
      <DialogActions>
        <Button variant="contained" onClick={() => onAddRegionClick()}>
          Add
        </Button>
        <Button variant="outlined" onClick={() => setIsOpen(false)}>
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
};
