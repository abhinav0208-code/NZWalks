export async function GetAllRegions() {
  const token = localStorage.getItem("token");
  const response = await fetch("https://localhost:7163/api/Regions", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });
  return response;
}

export async function DeleteRegion(id) {
  const response = await fetch(`https://localhost:7163/api/Regions/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });

  return response;
}

export async function CreateRegion({ code, name, regionImageUrl }) {
  const response = await fetch("https://localhost:7163/api/Regions", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
    body: JSON.stringify({
      code: code,
      name: name,
      regionImageUrl: regionImageUrl,
    }),
  });

  return response;
}
