using System.Collections.Generic;
using UnityEngine;
public static class PoissonDiskSampling
{
    public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection = 30)
    {
        float cellSize = radius / Mathf.Sqrt(2f); // Trouve la taille d'une cellule
        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)]; //Define le nombre de cellules par rapport à leur taille et celle de la map
        List<Vector2> points = new List<Vector2>();// Les points valide
        List<Vector2> spawnPoints = new List<Vector2>(); // les spawns potentielle de points

        spawnPoints.Add(sampleRegionSize / 2); // Rajoute un point au milieu de la map
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex]; // Permet de choisir un point aleatoire et d'en faire le centre pour placer le prochain point
            bool candidateAccepted = false;
            for (int i = 0; i < numSamplesBeforeRejection; i++)// Avant de suppimer le centre
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);
                if (IsValid(candidate, sampleRegionSize, radius, cellSize, points, grid)) //Si le point est valide : 
                {
                    points.Add(candidate);  // Rajoute le point entant que point valide
                    spawnPoints.Add(candidate);// Rajoute le point entant que les spawns potentielle de points
                    grid[(int)( candidate.x / cellSize ), (int)( candidate.y / cellSize )] = points.Count;
                    candidateAccepted = true;
                    break; //recommence la boucle while
                }
            }
            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex); // Suprime le centre
            }
        }
        return points; // Quand les spawnPoints sont egale a 0
    }

    private static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float radius, float cellSize, List<Vector2> points, int[,] grid)
    {
        if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
        {
            //retrouve la cellule du point : 
            int cellX = (int)( candidate.x / cellSize );
            int cellY = (int)( candidate.y / cellSize );

            // Clamp, pour pas faire un outOfRange : 
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndtX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);

            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndtY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            //Met en conditions les 5x5 cellule autour de candidate :
            for (int x = searchStartX; x <= searchEndtX; x++)
            {
                for (int y = searchStartY; y <= searchEndtY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrDst = ( candidate - points[pointIndex] ).sqrMagnitude;
                        if (sqrDst < radius * radius)// Pour chaque points on regarde si la distance est bonne
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}


//https://www.youtube.com/watch?v=7WcmyxyFO7o