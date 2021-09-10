import axios from "axios";
import { config } from "dotenv";

config({ path: "../.env" });

// const spotifyRequest = async () => {
//   const client_id = process.env.CLIENT_ID; // Your client id
//   const client_secret = process.env.CLIENT_SECRET; // Your secret
//   const auth_token = Buffer.from(`${client_id}:${client_secret}`, "utf-8").toString(
//     "base64"
//   );
//   const body = new URLSearchParams({ grant_type: "client_credentials" });

//   const tokenResponse = await axios.post("https://accounts.spotify.com/api/token", body, {
//     headers: {
//       Authorization: `Basic ${auth_token}`,
//       "Content-Type": "application/x-www-form-urlencoded",
//     },
//   });

//   const response = await axios.get(
//     "https://api.spotify.com/v1/browse/featured-playlists",
//     {
//       headers: {
//         Authorization: `Bearer ${tokenResponse.data.access_token}`,
//       },
//     }
//   );

//   console.log(response.data);
// };

// spotifyRequest();

interface AccessTokens {
  access_token: string;
  token_type: string;
  expires_in: number;
}

const clientCredentialsAccessToken = (): Promise<AccessTokens> => {
  const client_id = process.env.CLIENT_ID;
  const client_secret = process.env.CLIENT_SECRET;
  const auth_token = Buffer.from(`${client_id}:${client_secret}`, "utf-8").toString(
    "base64"
  );
  const body = new URLSearchParams({ grant_type: "client_credentials" });

  return axios
    .post("https://accounts.spotify.com/api/token", body, {
      headers: {
        Authorization: `Basic ${auth_token}`,
        "Content-Type": "application/x-www-form-urlencoded",
      },
    })
    .then((res) => {
      return res.data;
    });
};

const featurePlaylists = async (accessToken: Promise<AccessTokens>) => {
  const tokens = await accessToken;
  const response = await axios.get(
    "https://api.spotify.com/v1/browse/featured-playlists",
    {
      headers: {
        Authorization: `Bearer ${tokens.access_token}`,
      },
    }
  );

  console.log(response.data);
};

const requestFeaturePlaylists = () => {
  featurePlaylists(clientCredentialsAccessToken());
};

requestFeaturePlaylists();
