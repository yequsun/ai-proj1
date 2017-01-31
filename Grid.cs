using System;
using System.Collections;

public class Grid {
	private Node[,] grid = new Node[120,160];
	private ArrayList centers = new ArrayList();
	private string start = "";
	private string end = "";

	private class Node {
		private char cell;
		private int x;
		private int y;
		public int f = 0;
		public int g = 0;
		public int h = 0;

		public Node() {
			cell = '1';
		}

		public Node(char c) {
			cell = c;
		}
		
		public void SetCell(char c) {
			cell = c;
		}

		public char GetCell() {
			return cell;
		}
	}

	public Grid() {
		for (int i = 0; i < 120; i++) {
			for (int j = 0; j < 160; j++) {
				grid[i,j] = new Node();
			}
		}
		SetHardCells();
		// SetHighways();
		SetBlockedCells();
		// SetStartAndGoal();
	}

	public Grid(string path) {
		string[] lines = System.IO.File.ReadAllLines(path);
		start = lines[0];
		end = lines[1];
		for (int i = 2; i < 10; i++) {
			centers.Add(lines[i]);
		}
		for (int i = 10; i < 130; i++) {
			for (int j = 0; j < 320; j += 2) {
				grid[i-10, j/2] = new Node(lines[i][j]);
			}
		}
	}

	private void SetHardCells() {
		int row = 0, col = 0;
		Random rand = new Random();
		while (centers.Count < 8) {
			row = rand.Next(0, 120);
			col = rand.Next(0, 160);
			string pair = row.ToString() + " " + col.ToString();
			if (centers.Contains(pair)) continue;
			centers.Add(pair);
			for (int i = row-15; i <= row+15; i++) {
				if (i >= 120) break;
				if (i < 0) i = 0;
				for (int j = col-15; j <= col+15; j++) {
					if (j >= 160) break;
					if (j < 0) j = 0;
					if (rand.Next(0,2) == 1) {
						grid[i, j].SetCell('2');
					}
				}
			}
		}
	}
	
	private bool SetHighways() {
		//draws one single highway
		int direction = 0; //initial direction and boudary
		int x = 0; // origin x coordinate
		int y = 0; // origin y coordinate
		Random rand = new Random();
		direction = rand.Next(0,4);//0 top-down 1 right-left 2 bottom-up 3 left-right
		
		//set origin coordinates. Corners excluded.
		switch(direction){
			case 0:
				x = rand.Next(1,159);
				y = 0;
				break;
			case 1:
				x = 159;
				y = rand.Next(1,119);
				break;
			case 2:
				x = rand.Next(1,159);
				y = 119;
				break;
			case 3:
				x = 0;
				y = rand.Next(1,119);
		}

		Node cur = gird[y,x];
		int length = 20;
		do{
			cur = HighwaySegment(cur,direction);
			if(cur==null){
				EraseHighway();
			}else{
				length+=20;
			}
			//change direction
		}while(!BoundaryCheck(cur));

		if(length<100){
			EraseHighway();
			return false;
		}else{
			FinalizeHighway();
			return true;
		}

	}
	
	private Node HighwaySegment(Node cur,int d){
		//draws a segment of highway with length of 20
		Node cur;
		int x = cur.GetX();
		int y = cur.GetY();
		for(int i=0;i<20;i++){
			switch(d){
				case 0:
					cur = grid[y+i,x];
					break;
				case 1:
					cur = grid[y,x-i];
					break;
				case 2:
					cur = grid[y-i,x];
					break;
				case 3:
					cur = grid[y,x+i];
			}
			if(cur.GetCell() == 'a' || cur.GetCell() == 'b' || cur.GetCell() == 'A' || cur.GetCell() == 'B'){
				return null;
			}
			if(cur.GetCell() == '1'){
				cur.SetCell('A');
			}
			if(cur.GetCell() == '2'){
				cur.SetCell('B');
			}
		}
	}

	private bool BoundaryCheck(Node cur){
		int x = cur.GetX();
		int y = cur.GetY();
		if(x == 0 || y == 0 || x == 160 || y == 120){
			return true;
		}else{
			return false;
		}
	}

	private void EraseHighway(){
		Node cur;
		for(int i=0;i<120;i++){
			for(int j=0;j<160;j++){
				cur = grid[i,j];
				if(cur.GetCell=='A'){
					cur.SetCell('1');
				}
				if(cur.GetCell=='B'){
					cur.SetCell('2');
				}
			}
		}
	}

	private void FinalizeHighway(){
		Node cur;
		for(int i=0;i<120;i++){
			for(int j=0;j<160;j++){
				cur = grid[i,j];
				if(cur.GetCell=='A'){
					cur.SetCell('a');
				}
				if(cur.GetCell=='B'){
					cur.SetCell('b');
				}
			}
		}
	}

	private void SetBlockedCells() {
		int i = 0, row = 0, col = 0;
		Random rand = new Random();
		while (i <3840) {
			row = rand.Next(0, 120);
			col = rand.Next(0, 120);
			if (grid[row, col].GetCell() == '1' || grid[row, col].GetCell() == '2') {
				grid[row, col].SetCell('0');
				i++;
			}
		}
	}

	// private void SetStartAndGoal() {

	// }

	public void OutputGrid(string path) {
		using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path)) {
			file.WriteLine("start");
			file.WriteLine("end");
			foreach (string s in centers) {
				file.WriteLine(s);
			}
			for (int i = 0; i < 120; i++) {
				for (int j = 0; j < 160; j++) {
					file.Write(grid[i,j].GetCell()+" ");
				}
				file.WriteLine();
			}
		}
	}

	public static int Main(string[] args) {
		return 0;
	}
}
