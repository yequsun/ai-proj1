import java.io.*;
import java.util.Random;
import java.util.Arrays;
import java.util.ArrayList;

public class Grid{
	final private char[][] grid;
	private ArrayList<String> hardCenters = new ArrayList<>();
	final private String start;
	final private String end;

	public Grid() {
		this.grid = new char[120][160];
		for (char[] row: this.grid)
			Arrays.fill(row, '1');
		setHardCells();
		// setHighways();
		setBlockedCells();
		// setStartAndGoal();
	}
	
	public Grid(String path) {
		// write grid to file in format specified by project
	}

	private void setHardCells() {
		int row = 0;
		int col = 0;
		Random rand = new Random();
		while (this.hardCenters.size() < 8) {
			row = rand.nextInt(120);
			col = rand.nextInt(160);
			if (this.hardCenters.contains(Integer.toString(row) + ' ' + Integer.toString(col)))
				continue;
			this.hardCenters.add(Integer.toString(row) + ' ' + Integer.toString(col));
			for (int i = row-15; i <= row+15; i++) {
				if (i >= 120) break;
				if (i < 0) i = 0;
				for (int j = col-15; j <= col+15; j++) {
					if (j >= 160) break;
					if (j < 0) j = 0;
					if (rand.nextInt(2) == 1)
						this.grid[i][j] = '2';
				}
			}
		}
	}

	private void setHighways() {
		// select 4 boundary coords, move 20 cells at a time in a direction
		// (60% forward, 20% left, 20% right) until a boundary is reached.
		// If a previously placed highway is reached, restart.
		// If length of highway is <100, restart.
	}

	private void setBlockedCells() {
		int i = 0;
		int row = 0;
		int col = 0;
		Random rand = new Random();
		while (i < 3840) {
			row = rand.nextInt(120);
			col = rand.nextInt(160);
			if (this.grid[row][col] == '1' || this.grid[row][col] == '2') {
				this.grid[row][col] = '0';
				i++;
			}
		}
	}

	private void setStartAndGoal() {
		// corner of grid, region of 20 max.
		// If distance between start and goal <100, replace goal.
	}

	public void outputFile(String path) throws FileNotFoundException {
	   try {
			File f = new File(path);
			PrintWriter out = new PrintWriter(f);
			out.println("start placeholder");
			out.println("end placeholder");
			for (String s : this.hardCenters)
				out.println(s);
			for (char[] row : this.grid) {
				for (char c : row)
					out.print(Character.toString(c) + " ");
				out.println();
			}
			out.close();
	   } catch (FileNotFoundException ex) {}
	}

	public static void main(String args[]) throws FileNotFoundException {
		Grid g = new Grid();
		path = "/home/arnold/Documents/rutgers/CS440/grid.txt" // change to your filepath
		g.outputFile();
	}
}
