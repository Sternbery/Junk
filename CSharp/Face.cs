using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace polyhedraV3{
	public class Face{
		
		private int data;
		private Tuple<int,int>[] sides = new Tuple<int,int>[0];
		
		public Face(){}
		public Face(int d, Tuple<int,int>[] mysides){
			this.data = d;
			this.sides = mysides;
		}
		
		public void Add(int v, int e){
			this.Add(Tuple.Create(v,e));
		}
		
		public void Add(Tuple<int, int> ev){
			Tuple<int,int>[] newsides = new Tuple<int, int>[sides.Length+1];
			sides.CopyTo(newsides,0);
			newsides[sides.Length] = ev;
			sides = newsides;
			
		}
		
		public Tuple<int,int> this[int index]{
			get{
				return sides[index];
			}
		}
		public int Count(){
			return sides.Length;
		}
		public List<Tuple<int,int>> ToList{
			get{
				return sides.ToList();
			}
		}
		
		public int Data{
			get{
				return this.data;
			}
		}
		public List<int> Vertices {
			get{
				return sides.ToList().Select(s=>s.Item1).ToList();
			}
		}
		public List<int> Edges {
			get {
				return sides.ToList().Select(s=>s.Item2).ToList();
			}
		}
		
		// public List<Tuple<int,int,int>> Triangles{
			// get{
				// List<Tuple<int,int,int>> tris = new List<Tuple<int,int,int>>();
				// for(int v =1; v+1<this.Count();v++){
					// tris.Add(Tuple.Create(
						// this.Vertices[0],
						// this.Vertices[v],
						// this.Vertices[v+1]
						// ));
					
				// }
				// return tris;
			// }
		// }
		
		public F GetData<F,E,V>(FaceHedron<F,E,V> parent){
			return parent.FaceData[this.data];
		}
		public V GetVertex<F,E,V>(int index, FaceHedron<F,E,V> parent){
			return parent.VertexData[sides[index].Item1];
		}
		public List<V> GetVertices<F,E,V>(FaceHedron<F,E,V> parent){
			return sides.Select(t=>parent.VertexData[t.Item1]).ToList();
		}
	}
}

